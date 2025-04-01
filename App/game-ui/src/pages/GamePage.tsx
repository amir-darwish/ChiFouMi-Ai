

import React, {useEffect, useState} from 'react';
import { startGameSession, playGame, getSessionResult, getSessionHistory,BASE_URL } from '../services/gameService.ts';
import type { GameSessionHistory } from '../services/gameService.ts';


enum enShapeType {
    Rond = 1,
    Triangle = 2,
    Rectangle = 3
}

const GamePage: React.FC = () => {
    // Existing state
    const [playerName, setPlayerName] = useState<string>('');
    const [totalRounds, setTotalRounds] = useState<number>(0);
    const [difficultyLevel, setDifficultyLevel] = useState<number>(1);
    const [gameSessionId, setGameSessionId] = useState<number | null>(null);
    const [playerShape, setPlayerShape] = useState<number | null>(null);
    const [computerShape, setComputerShape] = useState<number | null>(null);
    const [result, setResult] = useState<string>('');
    const [roundsPlayed, setRoundsPlayed] = useState<number>(0);
    const [playerWins, setPlayerWins] = useState<number | null>(null);
    const [computerWins, setComputerWins] = useState<number | null>(null);
    const [showComputerChoice, setShowComputerChoice] = useState(false);
    const [gameEnded, setGameEnded] = useState(false);

    // New history state
    const [searchSessionId, setSearchSessionId] = useState<number | ''>('');
    const [sessionHistory, setSessionHistory] = useState<GameSessionHistory[]>([]);
    const [historyError, setHistoryError] = useState<string>('');
    const [availableSessions, setAvailableSessions] = useState<number[]>([]);
    const [isLoadingSessions, setIsLoadingSessions] = useState(false);

    // Add history search handler
    const handleSearchHistory = async (sessionId: number) => {
        setSessionHistory([]);
        setHistoryError('');

        try {
            const history = await getSessionHistory(sessionId);

            if (history && history.length > 0) {
                setSessionHistory(history);
            } else {
                setHistoryError(`No matches found for session #${sessionId}`);
            }
        } catch (error) {
            setHistoryError('Error fetching session history');
            console.error(error);
        }
    };
    //
    useEffect(() => {
        const fetchAllSessions = async () => {
            setIsLoadingSessions(true);
            try {
                const response = await fetch(`${BASE_URL}/sessions`);
                const data = await response.json();
                setAvailableSessions(data);
            } catch (error) {
                console.error("Error fetching sessions:", error);
            } finally {
                setIsLoadingSessions(false);
            }
        };

        fetchAllSessions();
    }, []);

    const handleStartGame = async () => {
        const sessionData = await startGameSession(playerName, totalRounds, difficultyLevel);
        if (sessionData?.sessionId) {
            setGameSessionId(sessionData.sessionId);
            setRoundsPlayed(0);
            setPlayerWins(null);
            setComputerWins(null);
            setComputerShape(null);
        }
    };

    const handlePlayGame = async () => {
        if (!gameSessionId || playerShape === null) return;

        setShowComputerChoice(false);
        const playData = await playGame(gameSessionId, playerShape);

        if (playData) {
            setComputerShape(playData.computerShape);

            setTimeout(() => {
                setShowComputerChoice(true);
                setResult(`${playData.result}`);
                setRoundsPlayed(prev => prev + 1);

                if (roundsPlayed + 1 >= totalRounds) {
                    fetchSessionResult(gameSessionId);
                    setGameEnded(true); 
                }
            }, 1000);
        }
    };
    
    

    const fetchSessionResult = async (sessionId: number) => {
        const sessionData = await getSessionResult(sessionId);
        if (sessionData) {
            setPlayerWins(sessionData.playerWins);
            setComputerWins(sessionData.computerWins);
        }
    };
    const resetGame = () => {
        setGameSessionId(null);
        setRoundsPlayed(0);
        setPlayerWins(null);
        setComputerWins(null);
        setComputerShape(null);
        setResult('');
        setGameEnded(false);
    };

    const getShapeComponent = (shape: number | null, isWinner: boolean = false) => {
        if (!shape) return null;

        const size = 120;
        const stroke = isWinner ? "url(#metal-gradient)" : "#00b4d8";

        return (
            <svg width={size} height={size} style={{ filter: isWinner ? 'url(#winner-glow)' : 'none' }}>
                <defs>
                    <linearGradient id="metal-gradient" x1="0%" y1="0%" x2="100%" y2="100%">
                        <stop offset="0%" stopColor="#a0a0a0" />
                        <stop offset="50%" stopColor="#ffffff" />
                        <stop offset="100%" stopColor="#a0a0a0" />
                    </linearGradient>

                    <filter id="winner-glow">
                        <feGaussianBlur stdDeviation="4" result="glow"/>
                        <feMerge>
                            <feMergeNode in="glow"/>
                            <feMergeNode in="SourceGraphic"/>
                        </feMerge>
                    </filter>
                </defs>

                {shape === enShapeType.Rond && (
                    <>
                        <circle cx={size/2} cy={size/2} r={size/2 - 8}
                                fill="none" stroke={stroke} strokeWidth="10" />
                        <path
                            d={`M${size/4},${size/2} Q${size/2},${size/4} ${size*0.75},${size/2} Q${size/2},${size*0.75} ${size/4},${size/2}`}
                            stroke="#00b4d8"
                            strokeWidth="3"
                            fill="none"
                        />
                    </>
                )}

                {shape === enShapeType.Triangle && (
                    <>
                        <polygon
                            points={`
                                ${size/2},${size*0.1} 
                                ${size*0.1},${size*0.9} 
                                ${size*0.9},${size*0.9}
                            `}
                            fill="none"
                            stroke={stroke}
                            strokeWidth="10"
                        />
                        <line
                            x1={size/2} y1={size*0.3}
                            x2={size*0.7} y2={size*0.7}
                            stroke="#00b4d8"
                            strokeWidth="3"
                        />
                    </>
                )}

                {shape === enShapeType.Rectangle && (
                    <>
                        <rect
                            x={size*0.1} y={size*0.1}
                            width={size*0.8} height={size*0.8}
                            fill="none"
                            stroke={stroke}
                            strokeWidth="10"
                            rx="15"
                        />
                        <path
                            d={`M${size*0.3},${size*0.3} L${size*0.7},${size*0.7} M${size*0.7},${size*0.3} L${size*0.3},${size*0.7}`}
                            stroke="#00b4d8"
                            strokeWidth="3"
                        />
                    </>
                )}
            </svg>
        );
    };

    const determineFinalResult = () => {
        if (playerWins === null || computerWins === null) return null;
        if (playerWins > computerWins) return "🎉 VICTORY!";
        if (computerWins > playerWins) return "💔 DEFEAT!";
        return "⚔️ DRAW!";
    };

    return (
        <div className="game-container">
            <div className="game-header">
                <h1>SHAPE SHOWDOWN</h1>
                <p>Choose your weapon and crush the AI!</p>

               
            </div>

            {!gameSessionId ? (
                <div className="game-input-group">
                    <input
                        className="game-input"
                        type="text"
                        placeholder="Warrior Name"
                        value={playerName}
                        onChange={(e) => setPlayerName(e.target.value)}
                    />
                    <input
                        className="game-input"
                        type="number"
                        placeholder="Battle Rounds"
                        value={totalRounds}
                        onChange={(e) => setTotalRounds(parseInt(e.target.value))}
                    />
                    <select
                        className="game-input game-select"
                        onChange={(e) => setDifficultyLevel(parseInt(e.target.value))}
                    >
                        <option value={1}>Easy</option>
                        <option value={2}>Medium</option>
                        <option value={3}>Hard</option>
                    </select>
                    <button className="game-button" onClick={handleStartGame}>
                        START BATTLE
                    </button>
                    {/* History Search Section */}
                    <div className="history-section">
                        <p>Show History</p>
                        <div className="session-selector">
                            {isLoadingSessions ? (
                                <div className="loading-sessions">
                                    Loading available sessions...
                                </div>
                            ) : (
                                <select
                                    className="game-select"
                                    value={searchSessionId}
                                    onChange={(e) => {
                                        const sessionId = Number(e.target.value);
                                        setSearchSessionId(sessionId);
                                        handleSearchHistory(sessionId);
                                    }}
                                >
                                    <option value="">Select a session</option>
                                    {availableSessions.map(sessionId => (
                                        <option key={sessionId} value={sessionId}>
                                            Session #{sessionId}
                                        </option>
                                    ))}
                                </select>
                            )}
                        </div>

                        {historyError && <p className="error-message">{historyError}</p>}

                        {sessionHistory.length > 0 && (
                            <div className="history-results">
                                <h3>Session {searchSessionId} History</h3>
                                <div className="history-list">
                                    {sessionHistory.map((record) => (
                                        <div key={record.id} className="history-record">
                                            <p>
                                                <span className="player-name">{record.playerName}</span> chose
                                                <span className="shape-name">{record.playerShape}</span> vs AI's
                                                <span className="shape-name">{record.computerShape}</span>
                                            </p>
                                            <p className="result-line">Result: {record.result}</p>
                                            <p className="timestamp">{new Date(record.playedAt).toLocaleString()}</p>
                                        </div>
                                    ))}
                                </div>
                            </div>
                        )}

                        {!isLoadingSessions && availableSessions.length === 0 && (
                            <p className="no-sessions">No sessions available</p>
                        )}
                    </div>
                </div>
            ) : (
                <div>
                    {!gameEnded ? (
                        <>
                            <div className="session-info">
                                <h2>BATTLE #{gameSessionId}</h2>
                                <div className="progress-bar">
                                    <div
                                        className="progress-fill"
                                        style={{ width: `${(roundsPlayed / totalRounds) * 100}%` }}
                                    />
                                </div>
                                <p>Round {roundsPlayed} of {totalRounds}</p>
                            </div>

                            <div className="battle-arena">
                                <div className="player-choice">
                                    {playerShape && (
                                        <div className={`shape-container ${result.includes('win') ? 'winning-effect' : ''}`}>
                                            {getShapeComponent(playerShape, result.includes('win'))}
                                            <span className="choice-label">YOUR WEAPON</span>
                                        </div>
                                    )}
                                </div>

                                <div className="vs-label">⚡</div>

                                <div className="computer-choice">
                                    {showComputerChoice && computerShape && (
                                        <div className={`shape-container ${result.includes('lose') ? 'destroyed' : ''}`}>
                                            {getShapeComponent(computerShape, result.includes('lose'))}
                                            <span className="choice-label">AI'S WEAPON</span>
                                        </div>
                                    )}
                                </div>
                            </div>

                            <div className="shape-selection">
                                {Object.values(enShapeType)
                                    .filter(v => typeof v === 'number')
                                    .map((shape) => (
                                        <button
                                            key={shape}
                                            className={`shape-button ${playerShape === shape ? 'selected' : ''}`}
                                            onClick={() => setPlayerShape(shape as number)}
                                        >
                                            {getShapeComponent(shape as number)}
                                        </button>
                                    ))}
                            </div>

                            <button
                                className="game-button"
                                onClick={handlePlayGame}
                                disabled={roundsPlayed >= totalRounds}
                            >
                                {roundsPlayed >= totalRounds ? 'VIEW RESULTS' : 'ATTACK!'}
                            </button>

                            {result && (
                                <div className="game-result">
                                    <h3 className={result.includes('win') ? 'victory-effect' : 'defeat-effect'}>
                                        {result}
                                    </h3>
                                </div>
                            )}
                        </>
                    ) : (
                        <div className="final-result">
                            <h2 className={`${determineFinalResult()?.includes('VICTORY') ? 'victory-text' : 'defeat-text'}`}>
                                {determineFinalResult()}
                            </h2>
                            <div className="score-board">
                                <p className="player-score">Player: {playerWins} 👑</p>
                                <p className="ai-score">AI: {computerWins} 🤖</p>
                            </div>
                            <button
                                className="game-button replay-button"
                                onClick={resetGame}
                            >
                                PLAY AGAIN
                            </button>
                        </div>
                    )}
                </div>
            )}
        </div>
    );
};

export default GamePage;