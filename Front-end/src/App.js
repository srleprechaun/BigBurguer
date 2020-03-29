import React from 'react';
import Header from './components/header/header';
import Footer from './components/footer/footer';
import Routes from './routes';

import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <div className="App">
      <Header />
      <Routes />
      <Footer />
    </div>
  );
}

export default App;
