export interface User {
    id?: number;
    username: string;
    email: string;
    token?: string;
  }
  
  export interface LoginRequest {
    usernameOrEmail: string;
    password: string;
  }
  
  export interface SignupRequest {
    username: string;
    email: string;
    password: string;
    phoneNumber: string;
  }