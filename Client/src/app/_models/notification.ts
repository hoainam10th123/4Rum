export interface Notification{
    id: string;
    noiDung: string;
    dateCreated: Date;
    userId: string;
    userCommentId: string;
    displayNameComment: string;
    photoUrlComment: string;
    questionId: string;
    isRead: boolean;
    commentParentId: string;
    commentChildrentId:number;
}