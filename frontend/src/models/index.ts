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

export interface FormSaveRequest {
    id: string
    title: EventTitle,
    description: EventDescription,
    eventLocation: EventLocation,
    eventStartDate: EventStartDate,
    eventEndDate: EventEndDate,
    registrationOpenDate: RegistrationOpenDate,
    registrationCloseDate: RegistrationCloseDate,
    submitSuccess: boolean;
    errors: boolean;
}