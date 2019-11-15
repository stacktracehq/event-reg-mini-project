import React from "react";
import axios from "axios";
import { Event, EventId } from "../../models/models";
import { RouteComponentProps, Link } from "react-router-dom";

export class ViewEvent extends React.Component<RouteComponentProps<EventId>, Event> {
    constructor(props: RouteComponentProps<EventId>) {
        super(props);
        this.state = {
            id: "",
            title: {value: ""},
            description: {value: ""},
            eventLocation: {value: ""},
            eventStartDate: {value: ""},
            eventEndDate: {value: ""},
            registrationOpenDate: {value: ""},
            registrationCloseDate: {value: ""},
            history: this.props.history,
            location: this.props.location,
            match: this.props.match
        }
    }

    componentDidMount() {
        axios.request<Event>({
            url: `https://localhost:5001/v1/events/${this.props.match.params.id}`
        })
            .then(response => {
                const { data } = response;
                this.setState({
                    id: data.id,
                    title: data.title,
                    description: data.description,
                    eventLocation: data.eventLocation,
                    eventStartDate:data.eventStartDate,
                    eventEndDate: data.eventEndDate,
                    registrationOpenDate: data.registrationOpenDate,
                    registrationCloseDate: data.registrationCloseDate
                 });
            })
            .catch(error => {
                console.log(error)
            })

    }

    handleDelete() {
        console.log("click")
        axios.delete(
            `https://localhost:5001/v1/events/${this.state.id}`
        )
        .then(() => {
            const { history } = this.state;
            history.push("/");
        })
        .catch(error => {
            console.log(error);
        })
    }

    renderEvent() {
        if (this.state.title === {value: ""}) {
            return ( <h1>Event Loading...</h1> )
        } else {
            return (
                <div>
                    <h1>
                        {this.state.title.value}
                    </h1>
                    <p>
                        {this.state.description.value}
                    </p>
                    <p>
                        <strong>Event Location: </strong>
                        {this.state.eventLocation.value}
                    </p>
                    <p>
                        <strong>Event Start Date: </strong>
                        {this.state.eventStartDate.value.toString().substr(0,10)}
                    </p>
                    <p>
                        <strong>Event End Date: </strong>
                        {this.state.eventEndDate.value.toString().substr(0,10)}
                    </p>
                    <p>
                        <strong>Registration Open Date: </strong>
                        {this.state.registrationOpenDate.value.toString().substr(0,10)}
                    </p>
                    <p>
                        <strong>Registration Close Date: </strong>
                        {this.state.registrationCloseDate.value.toString().substr(0,10)}
                    </p>
                    <div>
                        <Link to={`/event/${this.state.id}/edit`}>Edit</Link>
                        <button
                                type="button"
                                className="delete"
                                onClick={() =>
                                    this.handleDelete()
                                }
                            >
                                Delete
                            </button>
                    </div>
                </div>
            )
        }
    }

    public render() {
        return (
            <div>
                {this.renderEvent()}
            </div>
        )
  }
}

export default ViewEvent;