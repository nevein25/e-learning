import { Instructor } from "./Instructor";

export interface Course {
    id: number;
    name: string;
    duration: number;
    description: string;
    price: number;
    language: string;
    uploadDate: Date;
    thumbnail: string;
    instructorId: number;
    categoryId: number;
    instructor: Instructor;

  }
