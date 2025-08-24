"use client"

import type React from "react"
import { useState } from "react"
import { useEvents } from "../hooks/useEvents"
import EventCard from "../components/EventCard"
import EventForm from "../components/EventForm"
import { Loader2, Calendar, Sparkles, Search } from "../components/icons"
import { Button } from "../components/ui/button"
import { Input } from "../components/ui/input"
import { EventStatus } from "../types/Event"

const statusMap: Record<string, EventStatus> = {
  Scheduled: EventStatus.Scheduled,
  Cancelled: EventStatus.Cancelled,
  Completed: EventStatus.Completed,
}

const EventsList: React.FC = () => {
  const [showForm, setShowForm] = useState(false)
  const [searchQuery, setSearchQuery] = useState("")

  const { events, loading, fetchEvents } = useEvents()

  const handleFormSuccess = () => {
    setShowForm(false)
    fetchEvents() // refresh the events list
  }

  const filteredEvents = Array.isArray(events)
    ? events.filter((e) =>
        e.title.toLowerCase().includes(searchQuery.toLowerCase()) ||
        e.description.toLowerCase().includes(searchQuery.toLowerCase())
      )
    : []

  const activeEventsCount = Array.isArray(events)
    ? events.filter((e) => statusMap[e.status] === EventStatus.Scheduled).length
    : 0

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-amber-50 via-white to-pink-50">
        <div className="text-center">
          <Loader2 className="w-12 h-12 animate-spin text-amber-500 mx-auto mb-4" />
          <p className="text-gray-600 text-lg">Loading amazing events...</p>
        </div>
      </div>
    )
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-amber-50 via-white to-pink-50">
      {/* Hero Section */}
      <div className="relative overflow-hidden bg-gradient-to-r from-amber-500 to-pink-500 text-white">
        <div className="absolute inset-0 bg-black/10" />
        <div className="relative max-w-7xl mx-auto px-6 py-20">
          <div className="text-center">
            <div className="flex items-center justify-center mb-6">
              <Sparkles className="w-8 h-8 mr-3 animate-pulse" />
              <h1 className="text-5xl font-bold">Discover Events</h1>
              <Sparkles className="w-8 h-8 ml-3 animate-pulse" />
            </div>
            <p className="text-xl text-white/90 mb-8 max-w-2xl mx-auto leading-relaxed">
              Find extraordinary experiences, connect with amazing people, and create unforgettable memories
            </p>

            {/* Search Bar */}
            <div className="max-w-2xl mx-auto flex gap-4">
              <div className="relative flex-1">
                <Search className="absolute left-4 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                <Input
                  value={searchQuery}
                  onChange={(e) => setSearchQuery(e.target.value)}
                  placeholder="Search events, locations, or categories..."
                  className="pl-12 h-12 bg-white/95 border-0 text-gray-900 placeholder:text-gray-500"
                />
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* Stats Section */}
      <div className="max-w-7xl mx-auto px-6 py-12">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8 mb-12">
          <div className="text-center glass-card rounded-2xl p-8">
            <div className="text-3xl font-bold gradient-text mb-2">{activeEventsCount}</div>
            <div className="text-gray-600">Active Events</div>
          </div>
          <div className="text-center glass-card rounded-2xl p-8">
            <div className="text-3xl font-bold gradient-text mb-2">156K+</div>
            <div className="text-gray-600">Happy Attendees</div>
          </div>
          <div className="text-center glass-card rounded-2xl p-8">
            <div className="text-3xl font-bold gradient-text mb-2">4.9★</div>
            <div className="text-gray-600">Average Rating</div>
          </div>
        </div>

        {showForm && (
          <div className="mb-12">
            <EventForm onSuccess={handleFormSuccess} />
          </div>
        )}

        {/* Events Grid */}
        <div className="flex items-center justify-between mb-8">
          <div className="flex items-center">
            <Calendar className="w-6 h-6 text-amber-500 mr-3" />
            <h2 className="text-3xl font-bold text-gray-900">Upcoming Events</h2>
          </div>
          <div className="flex items-center gap-4">
            <div className="text-sm text-gray-500">{filteredEvents.length} events found</div>
            <Button
              onClick={() => setShowForm(!showForm)}
              className="bg-gradient-to-r from-amber-500 to-pink-500 hover:from-amber-600 hover:to-pink-600 text-white font-semibold px-6 py-2 rounded-lg shadow-lg hover:shadow-xl transition-all duration-200"
            >
              {showForm ? "Cancel" : "Add Event"}
            </Button>
          </div>
        </div>

        {filteredEvents.length === 0 ? (
          <div className="text-center py-20">
            <Calendar className="w-16 h-16 text-gray-300 mx-auto mb-4" />
            <h3 className="text-xl font-semibold text-gray-600 mb-2">No events found</h3>
            <p className="text-gray-500">Check back later for exciting new events!</p>
          </div>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
            {filteredEvents.map((event) => (
              <EventCard key={event.id} event={event} />
            ))}
          </div>
        )}
      </div>
    </div>
  )
}

export default EventsList
