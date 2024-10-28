export interface CreatedBookmark{
    userId:number,
    tweetId:number,
    bookmarkedAt:Date
}
export interface BookmarkForCreation{
    userId:number,
    tweetId:number
}