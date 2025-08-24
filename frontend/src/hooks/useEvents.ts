import { useState, useEffect } from 'react';
import { Event } from '../types/Event';
import { getEvents, deleteEvent } from '../services/eventService';

export const useEvents = () => {
  const [events, setEvents] = useState<Event[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  const fetchEvents = async () => {
    setLoading(true);
    try {
      const data = await getEvents();
      setEvents(data);
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const removeEvent = async (id: string) => {
    try {
      await deleteEvent(id);
      setEvents(prev => prev.filter(e => e.id !== id));
    } catch (err) {
      console.error(err);
      alert('Error deleting event');
    }
  };

  useEffect(() => {
    fetchEvents();
  }, []);

  return { events, loading, fetchEvents, removeEvent };
};
