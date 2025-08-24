export enum EventStatus {
  Scheduled = 0,
  Cancelled = 1,
  Completed = 2
}

export interface Event {
  id?: string
  title: string
  description: string
  date: string
  capacity: number
  bookedSeats: number
  status: EventStatus
}
