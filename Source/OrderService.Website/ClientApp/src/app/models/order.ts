export class Order {
    id?: number;
    creationDate: string;
    finishDate?: string;
    location: string;
    price?: number;
    name: string;
    executorName?: string;
    customerName?: string;
    description: string;
    photoPath: string;
    workTypeName: string;
    customerPhoneNumber?: string;
    photoPaths?: string[];
}