export class UpdateComment{
    id: any;
    noiDung: string;
    constructor(commentId, content: string){
        this.id = commentId;
        this.noiDung = content;
    }
}