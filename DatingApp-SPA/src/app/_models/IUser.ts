import { IPhoto } from './IPhoto';

export interface IUser {
    id:number;
    username:string;
    email:string;
    knownAs:string;
    age:number;
    gender:string;
    created:Date;
    lastActive:Date;
    photoUrl:string;
    city:string;
    country:string;
    interests?:string;
    introduction?:string;
    lookingFor?:string;
    photos?:IPhoto[];

}
