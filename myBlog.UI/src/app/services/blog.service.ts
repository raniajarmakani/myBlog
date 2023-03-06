import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogCreate } from '../Models/Blog/blog-create.model';
import { BlogPaging } from '../Models/Blog/blog-paging.model';
import { Blog } from '../Models/Blog/blog.model';
import { PagedResult } from '../Models/Blog/page-result.model';

@Injectable({
  providedIn: 'root'
})
export class BlogService {

  constructor(private http: HttpClient) {
   }

   creat(model: BlogCreate) :Observable<Blog>{
     return this.http.post<Blog>('${environment/webApi}/Blog', model);
   }

   getAll(blogPaging:BlogPaging):Observable<PagedResult<Blog>>{
    return this.http.get<PagedResult<Blog>>('${environment/webApi}/Blog?Page=${blogPaging.page}&PageSize=${blogPaging.pageSize}');
   }

   get(blogId: number): Observable<Blog>{
    return this.http.get<Blog>('${environmet/webApi}/Blog/${blogId}');
   }

   getByUserId(userId:number): Observable<Blog[]>{
    return this.http.get<Blog[]>('${environmet/webApi}/Blog/${userId}');
   }

   getFamous() :Observable<Blog[]>{
    return this.http.get<Blog[]>('${environmet/webApi}/Blog/famous');

   }
   delete(blogId:number):Observable<number>{
    return this.http.get<number>('${environmet/webApi}/Blog/${blogId}');

   }

   }
