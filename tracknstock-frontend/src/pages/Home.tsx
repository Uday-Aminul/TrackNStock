import { useState } from 'react';
import '../App.css';
import {IProduct, PageEnum, dummyProductList} from '../Components/Product';
import ProductList from '../Components/ProductList';
import AddProduct from '../Components/AddProduct';


const Home=()=>{

    const [productList, setProductList]=useState(
        dummyProductList as IProduct[])

    const [shownPage, setShownPage]=useState(PageEnum.list);

    const onAddProductClickHnd=()=>{
        setShownPage(PageEnum.add);
    }


    return(
        <>
        <article className="Homepage">
            <header>    
                <h1><span className="Title">TrackNStock</span></h1>
            </header>
        </article>

        <section className='ProductTable'>
            {shownPage===PageEnum.list &&(
            <>
                <input type="button" value="Add Product" onClick={onAddProductClickHnd}/>
                <ProductList list={productList}/>
            </>
            )}
            {shownPage===PageEnum.add && <AddProduct/>}
        </section>
        
        </>
    );
};
export default Home;