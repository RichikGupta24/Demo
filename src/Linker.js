import React from 'react';
import {Switch, Route} from 'react-router-dom';
import App from './App';
import NewUser from './NewUser';
class List extends React.Component {
    render() {
        return (
            <div>
 <Route path = '/addUser' component= {NewUser} />          
 <Route path = '/addPet' component= {App} />
 
                <p>Please choose from the list below :-</p>
                <ul>
                <li><a href="/addUser">Add User</a></li>
                    <li><a href="/addPet">Add Pet</a></li>
                    
                </ul>
            </div>
        );
    }
}

export default List;