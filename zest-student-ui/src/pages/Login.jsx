import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { login, setToken, getToken } from "../services/api";

function Login() {
  const navigate = useNavigate();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (getToken()) {
      navigate("/students", { replace: true });
    }
  }, [navigate]);

  const handleSubmit = async (event) => {
    event.preventDefault();
    setError("");

    if (!username.trim() || !password.trim()) {
      setError("Please enter both username and password.");
      return;
    }

    setLoading(true);
    try {
      const response = await login(username.trim(), password);
      const token = response?.data?.token || response?.data?.Token;
      if (!token) {
        throw new Error("Authentication response did not return a token.");
      }

      setToken(token);
      navigate("/students", { replace: true });
    } catch (error) {
      if (error?.response?.status === 401) {
        setError("Invalid username or password.");
      } else {
        setError("Unable to reach the server. Please try again later.");
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="page-shell">
      <div className="card login-card">
        <h1>Student Portal Login</h1>
        <p className="subtitle">Please sign in to manage student records.</p>

        <form onSubmit={handleSubmit}>
          <label htmlFor="username">Username</label>
          <input
            id="username"
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            placeholder="admin"
            autoComplete="username"
          />

          <label htmlFor="password">Password</label>
          <input
            id="password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Enter your password"
            autoComplete="current-password"
          />

          {error && <div className="message error">{error}</div>}

          <button type="submit" className="primary-button" disabled={loading}>
            {loading ? "Signing in..." : "Sign In"}
          </button>
        </form>
      </div>
    </div>
  );
}

export default Login;
