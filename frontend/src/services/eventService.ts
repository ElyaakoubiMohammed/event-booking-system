import axios from 'axios';
import { Event } from '../types/Event';

const API_URL = process.env.REACT_APP_API_URL!;

export const getEvents = async (): Promise<Event[]> => {
  const response = await axios.get<Event[]>(API_URL);
  return response.data;
};

export const getEventById = async (id: string): Promise<Event> => {
  const response = await axios.get<Event>(`${API_URL}/${id}`);
  return response.data;
};

export const createEvent = async (event: Event): Promise<string> => {
  const response = await axios.post(API_URL, event);
  return response.data;
};

export const updateEvent = async (event: Event): Promise<string> => {
  const response = await axios.put(`${API_URL}/${event.id}`, event);
  return response.data;
};

export const deleteEvent = async (id: string): Promise<void> => {
  await axios.delete(`${API_URL}/${id}`);
};
