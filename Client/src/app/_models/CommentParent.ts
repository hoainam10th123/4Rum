export interface CommentParent{
    id: string;
    noiDung: string;
    datePosted: Date;
    displayName: string;
    photoUrl: string;
    userName: string;
    questionId: string;
    childrentComments: CommentChildren[];
}

export interface CommentChildren{
    id: number;
    noiDung: string;
    datePosted: Date;
    displayName: string;
    photoUrl: string;
    userName: string;
    parentId: string;
}