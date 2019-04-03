import { Status } from './status';

export class NewOrder {
    id?: number;
    workTypeId?: number;
    finishDate?: string;
    location: string;
    price?: number;
    name: string;
    description: string;
    photos: number[];
    customerPhoneNumber: string;
    status?: Status;
}
