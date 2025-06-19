export async function hashPassword(password) {
    const textEncoder = new TextEncoder();
    const data = textEncoder.encode(password); // Converte a string da senha em bytes

    // Calcula o hash SHA-256
    const hashBuffer = await crypto.subtle.digest('SHA-256', data);

    // Converte o ArrayBuffer do hash para uma string hexadecimal
    const hashArray = Array.from(new Uint8Array(hashBuffer));
    const hexHash = hashArray.map(b => b.toString(16).padStart(2, '0')).join('');

    return hexHash;
}