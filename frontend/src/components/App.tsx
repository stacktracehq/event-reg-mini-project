
import React from 'react';
import './App.css';
import {
    Route,
    Link,
    BrowserRouter as Router,
    Switch
} from "react-router-dom";
import NewEvent from "./NewEvent/NewEvent";
import EventList from "./EventList/EventList";
import ViewEvent from "./ViewEvent/ViewEvent";
import EditEvent from './EditEvent/EditEvent';

const App: React.FC = () => {
  return (
    <Router>
        <div>
            <ul>
                <li>
                    <Link to="/">All Events</Link>
                </li>
                <li>
                    <Link to="/new">Create New Event</Link>
                </li>
            </ul>
            <Switch>
                <Route exact path="/event/:id/edit" component={EditEvent} />
                <Route path="/event/:id" component={ViewEvent} />
                <Route path="/new" component={NewEvent} />
                <Route exact path="/" component={EventList} />
            </Switch>
        </div>
    </Router>
)
}

export default App;