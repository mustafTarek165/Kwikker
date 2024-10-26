export interface UserForCreation{
 userName:string,
 email:String,
 profilePicture:string |null,
 Bio: string | null

}

export interface CreatedUser{
    id:number,
    userName:string,
    email:String,
    profilePicture:string |null,
    Bio: string | null
   

}