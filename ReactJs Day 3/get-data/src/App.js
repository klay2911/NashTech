import './App.css';
import React, { useEffect, useState } from 'react';
import DetailPost from './components/DetailPost';
import axios from 'axios';


function App() {
  const [selectedValue, setSelectedValue] = useState(1);
  const [detailData, setDataDetails] = useState();
  useEffect(() => {
    getData();
  },[selectedValue]);

  const getData = () => {
    axios.get(`https://jsonplaceholder.typicode.com/posts/${selectedValue}`).then(
      (result) => {
      console.log(result.data);
      setDataDetails(result?.data);
    });
  };

  const 

  const handleSelectChange = (event) => {
    setSelectedValue(event.target.value);
};
return(
      <>
        <div>
          <select onChange={handleSelectChange}>
            <option value="">Select...</option>
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
          </select>
          {selectedValue && <p>Option selected: {selectedValue}</p>}
        </div>    
  
      <div>
        {selectedValue === '1' && <DetailPost data={detailData} />}
        {selectedValue === '2' && <DetailPost data={detailData} />}
        {selectedValue === '3' && <DetailPost data={detailData} />}
      </div>
      </>
    );
}

export default App;
