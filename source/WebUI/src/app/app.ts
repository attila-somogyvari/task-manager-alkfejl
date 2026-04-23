import { CommonModule } from '@angular/common';
import { Component, DestroyRef, computed, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

import { TaskItem, TaskStatus } from './models/task.model';
import { TaskApiService } from './services/task-api.service';

@Component({
  selector: 'app-root',
  imports: [CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  private readonly destroyRef = inject(DestroyRef);
  private readonly taskApiService = inject(TaskApiService);

  protected readonly searchTerm = signal('');
  protected readonly selectedStatus = signal<'All' | TaskStatus>('All');
  protected readonly availableStatuses: Array<'All' | TaskStatus> = [
    'All',
    'Todo',
    'InProgress',
    'Done',
  ];
  protected readonly tasks = signal<TaskItem[]>([]);
  protected readonly isLoading = signal(true);
  protected readonly errorMessage = signal('');

  protected readonly filteredTasks = computed(() => {
    const term = this.searchTerm().trim().toLowerCase();
    const status = this.selectedStatus();

    return this.tasks().filter((task) => {
      const matchesStatus = status === 'All' || task.status === status;
      const matchesTerm =
        term.length === 0 ||
        task.title.toLowerCase().includes(term) ||
        task.description.toLowerCase().includes(term);

      return matchesStatus && matchesTerm;
    });
  });

  protected readonly totalTasks = computed(() => this.tasks().length);
  protected readonly completedTasks = computed(
    () => this.tasks().filter((task) => task.status === 'Done').length,
  );
  protected readonly inProgressTasks = computed(
    () => this.tasks().filter((task) => task.status === 'InProgress').length,
  );

  constructor() {
    this.loadTasks();
  }

  protected updateSearchTerm(value: string): void {
    this.searchTerm.set(value);
  }

  protected selectStatus(status: 'All' | TaskStatus): void {
    this.selectedStatus.set(status);
  }

  protected refreshTasks(): void {
    this.loadTasks();
  }

  protected cycleTaskStatus(task: TaskItem): void {
    const nextStatus = this.getNextStatus(task.status);

    this.taskApiService
      .updateTaskStatus(task.id, nextStatus)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (updatedTask) => {
          this.tasks.update((tasks) =>
            tasks.map((item) => (item.id === updatedTask.id ? updatedTask : item)),
          );
        },
        error: () => {
          this.errorMessage.set('A statuszmodositas most nem sikerult.');
        },
      });
  }

  protected deleteTask(taskId: string): void {
    this.taskApiService
      .deleteTask(taskId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.tasks.update((tasks) => tasks.filter((task) => task.id !== taskId));
        },
        error: () => {
          this.errorMessage.set('A torles most nem sikerult.');
        },
      });
  }

  protected formatDueDate(value: string | null): string {
    if (!value) {
      return 'Nincs megadva';
    }

    return new Date(value).toLocaleDateString('hu-HU');
  }

  private loadTasks(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.taskApiService
      .getTasks()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (tasks) => {
          this.tasks.set(tasks);
          this.isLoading.set(false);
        },
        error: () => {
          this.errorMessage.set(
            'A backend jelenleg nem elerheto. Ellenorizd, hogy fut-e a TaskService a 5095-os porton.',
          );
          this.isLoading.set(false);
        },
      });
  }

  private getNextStatus(status: TaskStatus): TaskStatus {
    switch (status) {
      case 'Todo':
        return 'InProgress';
      case 'InProgress':
        return 'Done';
      default:
        return 'Todo';
    }
  }
}
