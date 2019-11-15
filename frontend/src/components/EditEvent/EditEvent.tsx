import React from "react";
import axios from "axios";
import { RouteComponentProps } from "react-router";
import { EventSaveRequest, EventId, Event } from "../../models/models";

export class EditEvent extends React.Component<RouteComponentProps<EventId>, EventSaveRequest> {
        constructor(props: RouteComponentProps<EventId>) {
        super(props);
        this.state = {
            event: null,
            submitSuccess: false,
            errors: false,
        }
    }

    componentDidMount() {
        axios.get<Event>(`https://localhost:5001/v1/events/${this.props.match.params.id}`)
            .then( response  =>{
                this.setState({
                    event: response.data
                })
            })
            .catch(error => {
                console.log(error)
            })
    }

    private processFormSubmission = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        await axios.put(`https://localhost:5001/v1/events/${this.props.match.params.id}`, this.state.event)
                .then(() => {
                    this.setState({ submitSuccess: true});
                    const { history } = this.props;
                    history.push(`/event/${this.props.match.params.id}`);
                })
                .catch(error => {
                    console.log(error);
                    this.setState({ errors: true });
                });
    }

    private updateEventName = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            event: {
                ...this.state.event!,
                title: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventDescription = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            event: {
                ...this.state.event!,
            description: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventLocation = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            event: {
                ...this.state.event!,
            eventLocation: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventStartDate = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            event: {
                ...this.state.event!,
            eventStartDate: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventEndDate = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            event: {
                ...this.state.event!,
            eventEndDate: { value: e.currentTarget.value }
            }
        })
    }
  private updateRegistrationOpenDate = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
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
        if (this.state.event !== null) {
            return (

                    <div>
                    <h2 className="new-event-header">Edit Event:  {this.state.event!.title.value}</h2>
                {!submitSuccess && (
                        <div className="alert alert-info" role="alert">
                            Fill the form below to edit your event
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
                                placeholder={ this.state.event!.title.value }
                            />
                        </div>
                        <div>
                            <label htmlFor="description">Event Description: </label>
                            <input
                                type="text"
                                id="description"
                                onChange={(e) => this.updateEventDescription(e)}
                                name="description"
                                placeholder={ this.state.event!.description.value }
                            />
                        </div>
                        <div>
                            <label htmlFor="eventLocation">Event Location:</label>
                            <input
                                type="text"
                                id="eventLocation"
                                onChange={(e) => this.updateEventLocation(e)}
                                name="eventLocation"
                                placeholder={this.state.event!.eventLocation.value}
                            />
                        </div>
                        <div>
                            <label htmlFor="eventStartDate">Event Start Date: </label>
                            <input
                                type="date"
                                id="eventStartDate"
                                onChange={(e) => this.updateEventStartDate(e)}
                                name="eventStartDate"
                                value={this.state.event!.eventStartDate.value.toString().substr(0,10)}
                            />
                        </div>
                    <div>
                        <label htmlFor="eventEndDate">Event End Date: </label>
                            <input
                                type="date"
                                id="eventEndDate"
                                onChange={(e) => this.updateEventEndDate(e)}
                                name="eventEndDate"
                                value={this.state.event!.eventEndDate.value.toString().substr(0,10)}
                            />
                        </div>

                        <div>
                            <label htmlFor="registrationOpenDate">Registration Open Date: </label>
                            <input
                                type="date"
                                id="registrationOpenDate"
                                onChange={(e) => this.updateRegistrationOpenDate(e)}
                                name="registrationOpenDate"
                                value={this.state.event!.registrationOpenDate.value.toString().substr(0,10)}
                            />
                        </div>
                        <div>
                            <label htmlFor="registrationCloseDate">Registration Close Date: </label>
                            <input
                                type="date"
                                id="registrationCloseDate"
                                onChange={(e) => this.updateRegistrationCloseDate(e)}
                                name="registrationCloseDate"
                                value={this.state.event!.registrationCloseDate.value.toString().substr(0,10)}
                            />
                        </div>

                        <div>
                            <button type="submit" className="submit">
                                Update Event
                            </button>

                        </div>
                    </form>
                </div>


            )
        } else {
            return (
                <div>
                    Loading...
                </div>
            )
        }
    }

}

export default EditEvent;