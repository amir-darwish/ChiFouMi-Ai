export interface GameSessionHistory {
    id: number;
    playerName: string;
    playerShape: string;
    computerShape: string;
    result: string;
    gameSessionId: number;
    playedAt: string;
}
export const BASE_URL = 'http://localhost:5044/api/Game';

export const startGameSession = async (playerName: string, totalRounds: number, difficultyLevel: number) => {
    try {
        const response = await fetch(`${BASE_URL}/start-session`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ playerName, totalRounds, difficultyLevel }),
        });

        if (!response.ok) {
            throw new Error('Failed to start game session');
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error("Error starting game session:", error);
        return null;
    }
};

export const playGame = async (gameSessionId: number, playerShape: number) => {
    try {
        const response = await fetch(`${BASE_URL}/play`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ GameSessionId: gameSessionId, PlayerShape: playerShape }),
        });

        if (!response.ok) {
            throw new Error('Failed to play game');
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error("Error playing game:", error);
        return null;
    }
};
export const getSessionResult = async (sessionId: number) => {
    try{
        const response = await fetch(`${BASE_URL}/session-result/${sessionId}`);
        if (!response.ok) {
            throw new Error('Failed to get session result');
        }
        const data = await response.json();
        return data;
    }catch(error) {
        console.error("Error getting session result:", error);
        return null;
    }
    
}
export const getSessionHistory = async (sessionId: number): Promise<GameSessionHistory[] | null> => {
    try {
        const response = await fetch(`${BASE_URL}/session/${sessionId}`);

        if (!response.ok) {
            throw new Error('Failed to get session history');
        }

        const data: GameSessionHistory[] = await response.json();
        return data;
    } catch (error) {
        console.error("Error fetching session history:", error);
        return null;
    }
};




