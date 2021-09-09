import { CommentParent } from "./CommentParent";
import { QuestionTag } from "./questiontag";

export interface Question{
    id: string;
    tittle: string;
    datePosted: Date;
    viewCount: bigint;
    noiDung: string;
    userName: string;
    displayName: string;
    photoUrl: string;
    questionTags: QuestionTag[];
    commentParents: CommentParent[];
}

export interface QuestionAtHome{
    id: string;
    tittle: string;
    datePosted: Date;
    viewCount: bigint;
    noiDung: string;
    userName: string;
    displayName: string;
    photoUrl: string;
    questionTags: QuestionTag[];
    countCommentParents: number;
}

export interface SearchQuestion{
    tittle: string;
}