export interface ReviewInput {
    text: string;
    courseId: number;
}

export interface Review {
    id: number;
    text: string;
    courseId: number;
    // studentId:number;
}