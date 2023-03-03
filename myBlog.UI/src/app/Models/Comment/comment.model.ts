export class Comment {

    constructor(
        public CommentId: number,
        public blogId: number,
        public content: string,
        public username: string,
        public UserId: number,
        public publishDate: Date,
        public updateDate: Date,
        public parentCommentId?: number
    ) {}

}