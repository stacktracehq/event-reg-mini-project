import React from "react";
import { EventDTOs, EventDTO } from "../../models/index";
import axios from "axios";


export class AllEvents extends React.Component<{}, EventDTOs> {
    constructor(props: any) {
        super(props);
        this.state = {
            events: [ { id: "", title: "" } ]
        }
    }

     componentDidMount() {
        axios.request<EventDTO[]>({
            url: 'https://localhost:5001/v1/events',

        })
            .then(response => {
                const { data } = response;
                this.setState({ events: data });
            })
            .catch(error => {
                console.log(error);
            })
    }

    public render() {
        return (
            <div>
                <h1>Carlie's Stupendous and Amazing Event Registration Project</h1>

                    {this.state.events.map((anEvent) => {

                        return (
                            <ul key={anEvent.id}>
                                <li>{anEvent.id}</li>
                                <li>{anEvent.title}</li>
                            </ul>
                        )
                    })}

            </div>
        )
    }
}

export default AllEvents;