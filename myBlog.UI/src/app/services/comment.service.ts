import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommentCreate } from '../Models/Comment/comment-create.model';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private http:HttpClient) {

   }

   create(model:CommentCreate) :Observable<Comment>{
      return this.http.post<Comment>('${environmet.webApi}/Comment', model);
   }

   delete(commentId:number) : Observable<number>{
     return this.http.delete<number>('${environmet.webApi}/Comment/${commentId}');

   }

   getAll(blogId : number): Observable<Comment[]>{
    return this.http.get<Comment[]>('${environmet.webApi}/Comment/${blogId}');
   }
}
