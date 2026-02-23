import { NavLink } from "react-router-dom";

const Navbar = () => {
  const navStyle = {
    display: "flex",
    gap: "20px",
    padding: "1rem",
    background: "#f4f4f4",
    borderBottom: "1px solid #ddd",
  };

  const activeLink = ({ isActive }: { isActive: boolean }) => ({
    fontWeight: isActive ? "bold" : "normal",
    color: isActive ? "#007bff" : "#333",
    textDecoration: "none",
  });

  return (
    <nav style={navStyle}>
      <NavLink to="/" style={activeLink}>
        Home
      </NavLink>
      <NavLink to="/login" style={activeLink}>
        Login
      </NavLink>
      <NavLink to="/register" style={activeLink}>
        Register
      </NavLink>
      <NavLink to="/reset-password" style={activeLink}>
        Reset
      </NavLink>
    </nav>
  );
};

export default Navbar;
