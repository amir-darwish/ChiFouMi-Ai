// src/App.tsx

/*
import React from 'react';
*/
import './App.css';
import GamePage from './pages/GamePage'; 

const App = () => {
    return (
        <>
            <link href="https://fonts.googleapis.com/css2?family=Orbitron:wght@500&display=swap" rel="stylesheet"/>
            <div className="App">
                <header className="App-header">
                    <h1>Welcome to ChiFouMi-Ai</h1>
                </header>

                <GamePage/>

            </div>
        </>
)
    ;
}

export default App;
