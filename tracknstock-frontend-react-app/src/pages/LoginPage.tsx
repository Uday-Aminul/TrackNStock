import React from "react";
import "../styles/LoginPage.css";

function LoginPage() {
  return (
    <div className="login-page">
      <div className="login-container">
        <img src="/Favicon.png" alt="Logo" />
        <h2>Welcome back!</h2>
        <p>
          Don’t have an account? <a href="#">Sign up</a>
        </p>

        <button>Continue with Google</button>

        <div className="or-line">or</div>

        <input type="email" placeholder="Email" />
        <input type="password" placeholder="Password" />
        <button>Log In</button>

        <div className="forgot">Forgot Password?</div>
      </div>
    </div>
  );
}
export default LoginPage;
