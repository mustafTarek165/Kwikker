export interface TweetForCreation
{
    mediaUrl: string | ArrayBuffer | null,
    content:string
}
export interface CreatedTweet
{
    id:number,
    mediaUrl:string|ArrayBuffer|null,
    content:string,
    userId:number,
    createdAt:Date,
    profilePicture:string |ArrayBuffer|null,
    userName:string,
    likesNumber:number,
    retweetsNumber:number,
    bookmarksNumber:number
}