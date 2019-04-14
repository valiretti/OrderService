import { RequestStatus } from './requestStatus';

export class Request {
  id?: number;
  creationDate: string;
  executorId?: number;
  executorName?: string;
  customerName?: string;
  orderName?: string;
  orderId?: number;
  message: string;
  status: RequestStatus;
}
