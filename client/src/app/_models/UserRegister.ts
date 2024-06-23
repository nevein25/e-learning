import { Role } from "./Roles.enum";

export interface UserRegister {
    email: String;
    name: String;
    username: String;
    password: String;
    biography?: String;
    role: Role;
}

