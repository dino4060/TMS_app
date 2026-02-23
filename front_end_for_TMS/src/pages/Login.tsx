// const Login = () => {
//   return (
//     <div>
//       <h2>Login</h2>
//       <form onSubmit={(e) => e.preventDefault()}>
//         <div>
//           <label htmlFor="email">Email</label>
//           <input id="email" type="email" />
//         </div>
//         <div>
//           <label htmlFor="password">Password</label>
//           <input id="password" type="password" />
//         </div>
//         <button type="submit">Sign In</button>
//       </form>
//     </div>
//   );
// };

// export default Login;

import { Link } from "react-router-dom";

const Login = () => (
  <div className="card">
    <h2 style={{ marginTop: 0 }}>Welcome back</h2>
    <p style={{ color: "#6b7280", fontSize: "0.9rem", marginBottom: "1.5rem" }}>
      Enter your credentials to access your account.
    </p>
    <form onSubmit={(e) => e.preventDefault()}>
      <label style={{ fontSize: "0.875rem", fontWeight: 500 }}>
        Email Address
      </label>
      <input type="email" placeholder="name@company.com" required />

      <div style={{ display: "flex", justifyContent: "space-between" }}>
        <label style={{ fontSize: "0.875rem", fontWeight: 500 }}>
          Password
        </label>
        <Link
          to="/reset-password"
          style={{
            fontSize: "0.875rem",
            color: "#2563eb",
            textDecoration: "none",
          }}
        >
          Forgot?
        </Link>
      </div>
      <input type="password" placeholder="••••••••" required />

      <button type="submit">Sign In</button>
    </form>
  </div>
);

export default Login;
