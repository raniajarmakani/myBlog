export class CommentCreate {

    constructor(
        public CommentId: number,
        public blogId: number,
        public content: string,
        public parentCommentId?: number
    ) {}

}