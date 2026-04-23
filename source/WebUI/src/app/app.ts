import { CommonModule } from '@angular/common';
import { Component, computed, signal } from '@angular/core';

type TaskStatus = 'Todo' | 'InProgress' | 'Done';
type TaskPriority = 'Low' | 'Medium' | 'High';

interface TaskItem {
  id: number;
  title: string;
  description: string;
  status: TaskStatus;
  priority: TaskPriority;
  dueDate: string;
}

@Component({
  selector: 'app-root',
  imports: [CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly searchTerm = signal('');
  protected readonly selectedStatus = signal<'All' | TaskStatus>('All');
  protected readonly availableStatuses: Array<'All' | TaskStatus> = [
    'All',
    'Todo',
    'InProgress',
    'Done',
  ];

  protected readonly tasks = signal<TaskItem[]>([
    {
      id: 1,
      title: 'TaskService vegpontok ellenorzese',
      description: 'A backend lista, letrehozas es statuszmodositas vegpontjainak atnezese.',
      status: 'InProgress',
      priority: 'High',
      dueDate: '2026-04-24',
    },
    {
      id: 2,
      title: 'MCP muveletek dokumentalasa',
      description: 'A tamogatott toolok rovid osszefoglalasa a bemutatohoz.',
      status: 'Todo',
      priority: 'Medium',
      dueDate: '2026-04-25',
    },
    {
      id: 3,
      title: 'Dev Container stabilizalasa',
      description: 'A fejlesztoi kornyezet vegleges ellenorzese es takaritasa.',
      status: 'Done',
      priority: 'High',
      dueDate: '2026-04-23',
    },
    {
      id: 4,
      title: 'WebUI alap nezet kialakitasa',
      description: 'Task lista, szuresek es vizualis alapok kialakitasa Angularban.',
      status: 'InProgress',
      priority: 'High',
      dueDate: '2026-04-26',
    },
  ]);

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

  protected updateSearchTerm(value: string): void {
    this.searchTerm.set(value);
  }

  protected selectStatus(status: 'All' | TaskStatus): void {
    this.selectedStatus.set(status);
  }
}
