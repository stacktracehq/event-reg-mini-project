import React from "react";
import axios from 'axios';
import { Guid } from "guid-typescript";

export interface EventTitle {
    value: string
}

export interface EventDescription {
    value: string
}

export interface EventLocation {
    value: string
}

export interface EventStartDate {
    value: Date
}

export interface EventEndDate {
    value: Date
}

export interface Values {
    id: Guid,
    title: EventTitle,
    description: EventDescription,
    eventLocation: EventLocation,
    eventStartDate: EventStartDate,
    eventEndDate: EventEndDate,
}

export interface FormState {
    [key: string]: any;
    values: Values[];
    submitSuccess: boolean;
}

export class Form extends React.Component<{}, FormState> {
    constructor(props: any) {
        super(props);
        this.state = {
            id: Guid.create(),
            title: {value: ""},
            description: {value: ""},
            eventLocation: {value: ""},
            eventStartDate: {value: ""},
            eventEndDate: {value: ""},
            values: [],
            submitSuccess: false,
        }
    }

    private processFormSubmission = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const formData = {
            id: this.state.id.value,
            title: {value: this.state.title},
            description: {value: this.state.description},
            eventLocation: {value: this.state.eventLocation},
            eventStartDate: {value: this.state.eventStartDate},
            eventEndDate: {value: this.state.eventEndDate},
        }
        this.setState({ submitSuccess: true, values: [...this.state.values, formData]});
        await axios.post(`https://localhost:5001/v1/events`, formData)
                .then(response => console.log(response))
                .catch(error => console.log(error));
        console.log("~*~ Form Details ~*~")
        console.log("Id: " + this.state.id);
        console.log("Event Name: " + this.state.title);
        console.log("Description: " + this.state.description);
        console.log("Location: " + this.state.eventLocation);
        console.log("Event Start Date: " + this.state.eventStartDate);
        console.log("Event End Date: " + this.state.eventEndDate);
        console.log(formData);
    }

    private handleInputChanges = (e: React.FormEvent<HTMLInputElement>) => {
        e.preventDefault();
        this.setState({
            [e.currentTarget.name]: e.currentTarget.value,
        })
    }

    public render() {
        const { submitSuccess} = this.state;
        return (
            <div>
                <h2>Create a New Event</h2>
                {!submitSuccess && (
                      <div className="alert alert-info" role="alert">
                          Fill the form below to create a new event
                  </div>
                  )}
                  {submitSuccess && (
                      <div className="alert alert-info" role="alert">
                          The form was successfully submitted!
                          </div>
                  )}
                <form onSubmit={this.processFormSubmission} noValidate={true}>
                    <div>
                        <label htmlFor="title">Event Name: </label>
                        <input
                            type="text"
                            id="title"
                            onChange={(e) => this.handleInputChanges(e)}
                            name="title"
                            placeholder="Enter a title for your event"
                        />
                    </div>
                    <div>
                        <label htmlFor="description">Event Description: </label>
                        <input
                            type="text"
                            id="description"
                            onChange={(e) => this.handleInputChanges(e)}
                            name="description"
                            placeholder="Description of your event"
                        />
                    </div>
                    <div>
                        <label htmlFor="eventLocation">Event Location:</label>
                        <input
                            type="text"
                            id="eventLocation"
                            onChange={(e) => this.handleInputChanges(e)}
                            name="eventLocation"
                            placeholder=""
                        />
                    </div>
                    <div>
                        <label htmlFor="eventStartDate">Event Start Date: </label>
                        <input
                            type="date"
                            id="eventStartDate"
                            onChange={(e) => this.handleInputChanges(e)}
                            name="eventStartDate"
                            placeholder=""
                        />
                    </div>
                <div>
                    <label htmlFor="eventEndDate">Event End Date: </label>
                        <input
                            type="date"
                            id="eventEndDate"
                            onChange={(e) => this.handleInputChanges(e)}
                            name="eventEndDate"
                            placeholder=""
                        />
                    </div>
                    <div>
                        <button type="submit">
                            Create an Event
                        </button>
                    </div>
                </form>
            </div>
        )
    }
}

export default Form;