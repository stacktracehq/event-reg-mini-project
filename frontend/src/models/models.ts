import { RouteComponentProps } from "react-router-dom";

export interface EventTitle {
    value: string
}

export interface EventDescription {
    value: string
}

export interface EventLocation {
    value: string
}

// These have string for the constructor - need to fix!
export interface EventStartDate {
    value: Date | string
}

export interface EventEndDate {
    value: Date | string
}

export interface RegistrationOpenDate {
    value: Date | string
}

export interface RegistrationCloseDate {
    value: Date | string
}

export interface EventSaveRequest {
    event: Event | null,
    errors: boolean;
}

export interface EventDTO {
    id: string,
    title: string
}

export interface Event {
    id: string,
    title: EventTitle,
    description: EventDescription,
    eventLocation: EventLocation,
    eventStartDate: EventStartDate,
    eventEndDate: EventEndDate,
    registrationOpenDate: RegistrationOpenDate,
    registrationCloseDate: RegistrationCloseDate,
}

export interface EventId {
    id: string
}