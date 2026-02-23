// const ResetPassword = () => {
//   return (
//     <div>
//       <h2>Reset Password</h2>
//       <p>Enter your email to receive a reset link.</p>
//       <form onSubmit={(e) => e.preventDefault()}>
//         <input type="email" placeholder="Email Address" />
//         <button type="submit">Send Link</button>
//       </form>
//     </div>
//   );
// };

// export default ResetPassword;

const ResetPassword = () => (
  <div className="card">
    <h2 style={{ marginTop: 0 }}>Reset Password</h2>
    <p style={{ color: "#6b7280", fontSize: "0.9rem" }}>
      We'll send a recovery link to your email.
    </p>
    <form onSubmit={(e) => e.preventDefault()}>
      <input type="email" placeholder="Enter your email" required />
      <button type="submit">Send Reset Link</button>
    </form>
  </div>
);

export default ResetPassword;
