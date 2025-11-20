import { useEffect, useState } from "react";
import axios from "axios";
import type { Product } from "../models/Product";
import ProductList from "../components/HomePage/ProductList";
import "../styles/SoldProductPage.css";

interface SoldProductsPageProps {
  onBackButtonClickHandle: () => void;
}

function SoldProductsPage({ onBackButtonClickHandle }: SoldProductsPageProps) {
  const [soldProducts, setSoldProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchSoldProducts = async () => {
      try {
        const response = await axios.get(
          "http://localhost:5113/api/products/sold"
        );
        setSoldProducts(response.data);
        setLoading(false);
      } catch (err) {
        console.error(err);
        setError("Failed to fetch sold products");
        setLoading(false);
      }
    };
    fetchSoldProducts();
  }, []);

  if (loading) return <div>Loading sold products...</div>;
  if (error) return <div>{error}</div>;

  return (
    <div className="product-card">
      <div className="product-card-header">
        <h2 className="product-card-title">Sold Products</h2>
        <div className="product-list-toolbar">
          <button className="back-btn" onClick={onBackButtonClickHandle}>
            Back
          </button>
        </div>
      </div>

      <div className="product-list-wrapper">
        <ProductList
          products={soldProducts}
          onProductDeleted={() => {}} // optional: no delete in sold view
          onEditProductClick={() => {}} // optional: no edit in sold view
        />
      </div>
    </div>
  );
}

export default SoldProductsPage;
