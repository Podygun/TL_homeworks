import { useState } from "react";

export default function MixedForm() {
    const [phoneNumber, setPhoneNumber] = useState("");
  
    const handlePhoneNumberChange = (event) => {
      // Some function to format the phone number
      const formattedNumber = formatPhoneNumber(event.target.value);
      setPhoneNumber(formattedNumber);
    };
  
    const handleSubmit = (event) => {
      event.preventDefault();
      const formData = new FormData(event.target);
      for (let [key, value] of formData.entries()) {
        console.log(`${key}: ${value}`);
      }
    };

  return (
      <form onSubmit={handleSubmit}>
        <label>Name:</label>
        <input type="text" name="name" />
  
        <label>Email:</label>
        <input type="email" name="email" />
  
        <label>Phone Number:</label>
        <input
          type="tel"
          name="phoneNumber"
          value={phoneNumber}
          onChange={handlePhoneNumberChange}
        />
  
        <label>Address:</label>
        <input type="text" name="address" />
  
        <button type="submit">Submit</button>
      </form>
    );
  }
