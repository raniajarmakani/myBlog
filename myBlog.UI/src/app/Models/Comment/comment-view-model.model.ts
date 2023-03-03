export class CommentViewModel {

    constructor(
        public parentCommentId: number,
        public CommentId: number,
        public blogId: number,
        public content: string,
        public username: string,
        public publishDate: Date,
        public updateDate: Date,
        public isEditable: boolean = false,
        public deleteConfirm: boolean = false,
        public isReplying: boolean = false,
        public comments: CommentViewModel[]
    ) {}

}