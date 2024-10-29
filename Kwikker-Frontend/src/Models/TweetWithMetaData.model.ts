
import { MetaData } from "./MetaData.model";
import { CreatedTweet } from "./Tweet.model";

export interface TweetWithMetaData{
    createdTweets:CreatedTweet[],
    metaData:MetaData
}