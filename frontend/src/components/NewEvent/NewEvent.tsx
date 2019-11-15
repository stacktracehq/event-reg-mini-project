import React from "react";
import axios from 'axios';
import {
    EventSaveRequest
} from "../../models/models"
import { RouteComponentProps } from "react-router-dom";

export class NewEvent extends React.Component<{} & RouteComponentProps, EventSaveRequest> {
    constructor(props: RouteComponentProps) {
        super(props);
        this.state = {
            event: null,
            submitSuccess: false,
            errors: false,
        }
    }

    private processFormSubmission = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        await axios.post(`https://localhost:5001/v1/events`, this.state)
                .then(response => {
                    this.setState({ submitSuccess: true});
                    const { history } = this.props;
                    history.push('/');
                })
                .catch(error => {
                    console.log(error);
                    this.setState({ errors: true });
                });
    }

    private updateEventName = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                title: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventDescription = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                description: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventLocation = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                eventLocation: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventStartDate = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                eventStartDate: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventEndDate = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                eventEndDate: { value: e.currentTarget.value }
            }
        })
    }

    private updateRegistrationOpenDate = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                registrationOpenDate: { value: e.currentTarget.value }
            }
        })
    }

    private updateRegistrationCloseDate = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                registrationCloseDate: { value: e.currentTarget.value }
            }
        })
    }

    public render() {
        const { submitSuccess, errors} = this.state;
        return (
            <div>
                <h2 className="new-event-header">Create a New Event</h2>
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
                    {errors && (
                      <div className="alert alert-info alert-error" role="alert">
                          Oops, something went wrong
                          </div>
                  )}
                <form onSubmit={this.processFormSubmission} noValidate={true}>
                    <div>
                        <label htmlFor="title">Event Name: </label>
                        <input
                            type="text"
                            id="title"
                            onChange={(e) => this.updateEventName(e)}
                            name="title"
                            placeholder="Enter a title for your event"
                        />
                    </div>
                    <div>
                        <label htmlFor="description">Event Description: </label>
                        <input
                            type="text"
                            id="description"
                            onChange={(e) => this.updateEventDescription(e)}
                            name="description"
                            placeholder="Description of your event"
                        />
                    </div>
                    <div>
                        <label htmlFor="eventLocation">Event Location:</label>
                        <input
                            type="text"
                            id="eventLocation"
                            onChange={(e) => this.updateEventLocation(e)}
                            name="eventLocation"
                            placeholder=""
                        />
                    </div>
                    <div>
                        <label htmlFor="eventStartDate">Event Start Date: </label>
                        <input
                            type="date"
                            id="eventStartDate"
                            onChange={(e) => this.updateEventStartDate(e)}
                            name="eventStartDate"
                            placeholder=""
                        />
                    </div>
                <div>
                    <label htmlFor="eventEndDate">Event End Date: </label>
                        <input
                            type="date"
                            id="eventEndDate"
                            onChange={(e) => this.updateEventEndDate(e)}
                            name="eventEndDate"
                            placeholder=""
                        />
                    </div>

                    <div>
                        <label htmlFor="registrationOpenDate">Registration Open Date: </label>
                        <input
                            type="date"
                            id="registrationOpenDate"
                            onChange={(e) => this.updateRegistrationOpenDate(e)}
                            name="registrationOpenDate"
                            placeholder=""
                        />
                    </div>
                    <div>
                        <label htmlFor="registrationCloseDate">Registration Close Date: </label>
                        <input
                            type="date"
                            id="registrationCloseDate"
                            onChange={(e) => this.updateRegistrationCloseDate(e)}
                            name="registrationCloseDate"
                            placeholder=""
                        />
                    </div>

                    <div>
                        <button type="submit" className="submit">
                            Create an Event
                        </button>

                    </div>
                </form>
            </div>
        )
    }
}

export default NewEvent;