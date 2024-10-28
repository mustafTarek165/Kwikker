export interface TweetForCreation
{
    mediaUrl: string | ArrayBuffer | null,
    content:string
}
export interface CreatedTweet
{
    id:number,
    mediaUrl:string,
    content:string,
    userId:number,
    createdAt:Date,
    profilePicture:string |ArrayBuffer|null,
    userName:string
}