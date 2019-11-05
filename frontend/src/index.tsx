import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import {
    Route,
    Link,
    BrowserRouter as Router
} from "react-router-dom";
import * as serviceWorker from './serviceWorker';
import Form from "./components/Form/Form";
import AllEvents from "./components/AllEvents/AllEvents";

const routing = (
    <Router>
        <div>
            <ul>
                <li>
                    <Link to="/">Home</Link>
                </li>
                <li>
                    <Link to="/new">Create New Event</Link>
                </li>
            </ul>

            <Route exact path="/" component={AllEvents} />
            <Route path="/new" component={Form} />
        </div>
    </Router>
)

ReactDOM.render(routing, document.getElementById('root'));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
