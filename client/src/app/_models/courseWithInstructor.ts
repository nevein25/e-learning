export interface CourseWithInstructor {
    id: number;
    name: string;
    duration: number;
    description: string;
    price: number;
    language: string;
    thumbnail: string;
    uploadDate: Date;
    instructorId: number;
    categoryId: number;
    instructorName: string;
    category: string;
}
