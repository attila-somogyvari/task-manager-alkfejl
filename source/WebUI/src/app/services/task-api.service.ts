import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

import { TaskItem, TaskStatus } from '../models/task.model';

@Injectable({
  providedIn: 'root',
})
export class TaskApiService {
  private readonly httpClient = inject(HttpClient);

  getTasks(status?: TaskStatus, search?: string): Observable<TaskItem[]> {
    let params = new HttpParams();

    if (status) {
      params = params.set('status', status);
    }

    if (search && search.trim().length > 0) {
      params = params.set('search', search.trim());
    }

    return this.httpClient.get<TaskItem[]>('/tasks', { params });
  }

  updateTaskStatus(id: string, status: TaskStatus): Observable<TaskItem> {
    return this.httpClient.patch<TaskItem>(`/tasks/${id}/status`, { status });
  }

  deleteTask(id: string): Observable<void> {
    return this.httpClient.delete<void>(`/tasks/${id}`);
  }
}
