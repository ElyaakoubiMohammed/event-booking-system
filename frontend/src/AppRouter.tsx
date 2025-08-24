import React from 'react';
import { Routes, Route } from 'react-router-dom';
import EventsList from './pages/EventsList';
import EventDetails from './pages/EventDetails';
import EventForm from './components/EventForm';
import { useEvents } from './hooks/useEvents';

const AppRouter: React.FC = () => {
  const { fetchEvents } = useEvents();

  return (
    <Routes>
  <Route path="/" element={<EventsList />} />
  <Route path="/events/:id" element={<EventDetails />} />  {/* updated path */}
  <Route path="/create" element={<EventForm onSuccess={fetchEvents} />} />
  <Route path="/edit/:id" element={<EventForm onSuccess={fetchEvents} />} />
</Routes>

  );
};

export default AppRouter;
