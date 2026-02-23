// const Register = () => {
//   return (
//     <div>
//       <h2>Create Account</h2>
//       <form onSubmit={(e) => e.preventDefault()}>
//         <input type="text" placeholder="Username" />
//         <input type="email" placeholder="Email" />
//         <input type="password" placeholder="Password" />
//         <button type="submit">Register</button>
//       </form>
//     </div>
//   );
// };

// export default Register;

const Register = () => (
  <div className="card">
    <h2 style={{ marginTop: 0 }}>Create account</h2>
    <form onSubmit={(e) => e.preventDefault()}>
      <label style={{ fontSize: "0.875rem" }}>Full Name</label>
      <input type="text" placeholder="John Doe" />
      <label style={{ fontSize: "0.875rem" }}>Email</label>
      <input type="email" placeholder="john@example.com" />
      <label style={{ fontSize: "0.875rem" }}>Password</label>
      <input type="password" placeholder="Min. 8 characters" />
      <button type="submit">Get Started</button>
    </form>
  </div>
);

export default Register;
