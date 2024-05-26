import React, { useState, useEffect } from 'react';
import Pokemon from './components/Pokemon';
import axios from 'axios';

function App() {
  const [selectedValue, setSelectedValue] = useState('');
  const [pokemonId, setPokemonId] = useState(1);
  const [pokemonData, setPokemonData] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    setIsLoading(true);
    axios.get(`https://pokeapi.co/api/v2/pokemon/${pokemonId}`)
      .then(response => {
        setPokemonData(response.data);
        setIsLoading(false);
      })
      .catch(err => {
        setError(err.message);
        setIsLoading(false);
      });
  }, [pokemonId]);

  const handleNext = () => {
    setPokemonId(prevId => prevId + 1);
  };

  const handlePrev = () => {
    if (pokemonId > 1) {
      setPokemonId(prevId => prevId - 1);
    }
  };

  const handleSelectChange = (event) => {
    setSelectedValue(event.target.value);
};
  return (
    <div>
      {isLoading ? (
        <p>Loading...</p>
      ) : error ? (
        <p>Error: {error}</p>
      ) : (
        <>
          <div style={{justifyItems: 'center'}}> 
            <select style={{width:'260px', height: '30px'}} value={selectedValue} onChange={handleSelectChange}>
              <option value="">Select...</option>
              <option value="Pokemon">pokemon</option>
            </select>
            {selectedValue && <p>Option selected: {selectedValue}</p>}
          </div> 
          <div>
            {selectedValue ==='Pokemon' && <Pokemon  data={pokemonData} />}
            <button style={{width:'100px',marginRight: '60px'}} onClick={handlePrev} disabled={pokemonId === 1}>Previous</button>
            <button style={{width:'100px'}}onClick={handleNext}>Next</button>
          </div> 
        </>
      )}
    </div>
  );
}

export default App;