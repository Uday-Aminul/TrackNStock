import { useEffect, useState } from "react";
import LoginPage from "./LoginPage";
import type { Product } from "../models/Product";
import axios from "axios";
import "../styles/HomePage.css";
import ProductList from "../components/HomePage/ProductList";
import AddProductPage from "./AddProductPage";
import UpdateProductPage from "./UpdateProductPage";
import SoldProductsPage from "./SoldProductPage";

enum PageEnum {
  loginPage,
  productListPage,
  addProductPage,
  updateProductPage,
  soldProductsPage,
}

function HomePage() {
  const [productList, setProductList] = useState([] as Product[]);

  const [editingProductId, setEditingProductId] = useState<number | null>(null);

  const [shownPage, setShownPage] = useState(PageEnum.productListPage);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const [searchId, setSearchId] = useState("");

  const onAddProductClickHandle = () => {
    setShownPage(PageEnum.addProductPage);
  };
  const showListPage = () => {
    setShownPage(PageEnum.productListPage);
  };
  const onEditProductClick = (id: number) => {
    setEditingProductId(id);
    setShownPage(PageEnum.updateProductPage);
  };
  const showUpdateProductPage = () => {
    setShownPage(PageEnum.productListPage);
    setEditingProductId(null);
  };
  const showSoldProductsPage = () => {
    setShownPage(PageEnum.soldProductsPage);
  };

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        //const token = localStorage.getItem("token"); // JWT from login
        const response = await axios.get(
          "http://localhost:5113/api/products" /*, {
                        headers: { Authorization: `Bearer ${token}` }
                    }*/
        );
        setProductList(response.data);
        setLoading(false);
      } catch (err) {
        console.error(err);
        setError("Failed to fetch products from API");
        setLoading(false);
      }
    };
    fetchProducts();
  }, []);

  const refreshProducts = async () => {
    try {
      const response = await axios.get("http://localhost:5113/api/products");
      setProductList(response.data);
    } catch (err) {
      console.error(err);
      alert("Failed to refresh products");
    }
  };

  useEffect(() => {
    if (searchId === "") refreshProducts();
  }, [searchId]);

  if (loading) return <div>Loading products...</div>;
  if (error) return <div>{error}</div>;

  return (
    <>
      <div className="homepage-container">
        <header className="homepage-header">
          <h1 className="homepage-title">TrackNStock</h1>
        </header>

        <section className="product-section">
          {shownPage === PageEnum.productListPage && (
            <div className="product-card">
              <div className="product-card-header">
                <h2 className="product-card-title">Available Products:</h2>
                <div className="product-list-toolbar">
                  <div className="search-wrapper">
                    <input
                      className="search-input"
                      type="number"
                      placeholder="Search by ID..."
                      aria-label="Search products"
                      value={searchId}
                      onChange={(e) => setSearchId(e.target.value)}
                    />
                    <button
                      className="search-btn"
                      title="Search"
                      onClick={() => {
                        if (searchId === "") return refreshProducts(); // show all if empty
                        const filtered = productList.filter(
                          (product) => product.id === Number(searchId)
                        );
                        setProductList(filtered);
                      }}
                    >
                      🔍
                    </button>
                  </div>
                  <button className="add-btn" onClick={onAddProductClickHandle}>
                    Add Product
                  </button>
                  <button
                    className="sold-btn"
                    title="View sold products"
                    onClick={showSoldProductsPage}
                  >
                    Sold Products
                  </button>
                </div>
              </div>

              <div className="product-list-wrapper">
                <ProductList
                  products={productList}
                  onProductDeleted={refreshProducts}
                  onEditProductClick={onEditProductClick}
                />
              </div>
            </div>
          )}

          {shownPage === PageEnum.updateProductPage && editingProductId && (
            <UpdateProductPage
              productId={editingProductId}
              onBackButtonClickHandle={showUpdateProductPage}
              onProductUpdated={refreshProducts}
            />
          )}

          {shownPage === PageEnum.soldProductsPage && (
            <SoldProductsPage onBackButtonClickHandle={showListPage} />
          )}

          {shownPage === PageEnum.loginPage && <LoginPage />}

          {shownPage === PageEnum.addProductPage && (
            <AddProductPage
              onBackButtonClickHandle={showListPage}
              onProductAdded={refreshProducts}
            />
          )}
        </section>
      </div>
    </>
  );
}

export default HomePage;
