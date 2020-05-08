import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './sytle.css'

function CadastroProduto() {
    return (
        <div id='cadastroProduto'>
            <h1>Cadastro de Produto</h1>
            <form action="../../Back-end/BigBurguer/BigBurguer.Api/Controllers/ProductsController.cs">
                <div>
                    <label id="lblNome">Nome:</label>
                    <input id="txtNome" type="text" class="form-control" />
                </div>
                <div>
                    <label id="lblValor">Valor:</label>
                    <input id="txtValor" type="number" class="form-control" />
                </div>
                <div>
                    <label id="lblIngredientes">Lista de Ingredientes:</label>
                    <input id="txtIngredientes" type="text" class="form-control" />
                </div>
                <div>
                    <label id="lblImgProduto">Imagem do Produto:</label>
                    <br></br>
                    <input id="imgProduto" type="file" />
                </div>
                <br></br>
                <input id="btnCadastroProduto" type="submit" class="btn btn-primary" value="Cadastrar Produto" />
            </form>
        </div>
    );
}

export default CadastroProduto;